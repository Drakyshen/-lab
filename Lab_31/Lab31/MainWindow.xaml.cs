using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Win32;

namespace WpfApp1
{
    public class ProcessViewModel
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public string MemoryKB { get; set; }
        public string VirtualMemoryMB { get; set; }
        public int ThreadCount { get; set; }
        public string StartTime { get; set; }
        public string MainModule { get; set; }
    }

    public class ThreadViewModel
    {
        public int Id { get; set; }
        public string PriorityLevel { get; set; }
        public int CurrentPriority { get; set; }
        public string StartTime { get; set; }
        public string StartAddress { get; set; }
    }

    public class ModuleViewModel
    {
        public string ModuleName { get; set; }
        public string FileName { get; set; }
        public string MemorySizeKB { get; set; }
        public string BaseAddress { get; set; }
    }

    public partial class MainWindow : Window
    {
        private List<ProcessViewModel> _allProcesses = new List<ProcessViewModel>();
        private DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            LoadProcesses();

            _timer.Interval = TimeSpan.FromSeconds(10);
            _timer.Tick += (s, e) => LoadProcesses();
            _timer.Start();
        }

        private void LoadProcesses()
        {
            _allProcesses.Clear();

            Process[] processes = Process.GetProcesses();
            Array.Sort(processes, (a, b) => string.Compare(a.ProcessName, b.ProcessName));

            foreach (Process proc in processes)
            {
                try
                {
                    string startTime = "—";
                    try { startTime = proc.StartTime.ToString("dd.MM.yyyy HH:mm:ss"); }
                    catch { }

                    string mainModule = "—";
                    try { mainModule = proc.MainModule != null ? proc.MainModule.ModuleName : "—"; }
                    catch { }

                    _allProcesses.Add(new ProcessViewModel
                    {
                        Id = proc.Id,
                        ProcessName = proc.ProcessName,
                        MemoryKB = (proc.PagedMemorySize64 / 1024).ToString("N0"),
                        VirtualMemoryMB = (proc.VirtualMemorySize64 / 1024 / 1024).ToString("N0"),
                        ThreadCount = proc.Threads.Count,
                        StartTime = startTime,
                        MainModule = mainModule
                    });
                }
                catch { }
            }

            ApplyFilter();
            txtProcessCount.Text = "  " + _allProcesses.Count + " процесів  ";
            txtLastUpdate.Text = "Оновлено: " + DateTime.Now.ToString("HH:mm:ss");
            txtStatus.Text = "Завантажено " + _allProcesses.Count + " процесів";
        }

        private void ApplyFilter()
        {
            string filter = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(filter))
            {
                lvProcesses.ItemsSource = _allProcesses;
            }
            else
            {
                var filtered = new List<ProcessViewModel>();
                foreach (var p in _allProcesses)
                {
                    if (p.ProcessName.ToLower().Contains(filter) ||
                        p.Id.ToString().Contains(filter))
                        filtered.Add(p);
                }
                lvProcesses.ItemsSource = filtered;
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void LvProcesses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProcessViewModel selected = lvProcesses.SelectedItem as ProcessViewModel;
            if (selected != null)
                ShowProcessInfo(selected.Id);
        }

        private void ShowProcessInfo(int processId)
        {
            try
            {
                Process proc = Process.GetProcessById(processId);

                lblId.Text = proc.Id.ToString();
                lblName.Text = proc.ProcessName;
                lblMachine.Text = proc.MachineName;

                try { lblStartTime.Text = proc.StartTime.ToString("dd.MM.yyyy HH:mm:ss"); }
                catch { lblStartTime.Text = "Недоступно"; }

                lblMemory.Text = (proc.PagedMemorySize64 / 1024).ToString("N0") + " KB";
                lblVirtMemory.Text = (proc.VirtualMemorySize64 / 1024 / 1024).ToString("N0") + " MB";

                try { lblMainModule.Text = proc.MainModule != null ? proc.MainModule.FileName : "—"; }
                catch { lblMainModule.Text = "Доступ заборонено"; }

                LoadThreads(proc);
                LoadModules(proc);

                txtStatus.Text = "Вибрано: " + proc.ProcessName + " (PID " + proc.Id + ")";
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Не вдалося отримати інформацію: " + ex.Message;
            }
        }

        private void LoadThreads(Process proc)
        {
            var threads = new List<ThreadViewModel>();
            foreach (ProcessThread t in proc.Threads)
            {
                string startTime = "—";
                try { startTime = t.StartTime.ToString("dd.MM.yyyy HH:mm:ss"); }
                catch { }

                threads.Add(new ThreadViewModel
                {
                    Id = t.Id,
                    PriorityLevel = t.PriorityLevel.ToString(),
                    CurrentPriority = t.CurrentPriority,
                    StartTime = startTime,
                    StartAddress = "0x" + t.StartAddress.ToString("X")
                });
            }
            lvThreads.ItemsSource = threads;
        }

        private void LoadModules(Process proc)
        {
            var modules = new List<ModuleViewModel>();
            try
            {
                foreach (ProcessModule m in proc.Modules)
                {
                    modules.Add(new ModuleViewModel
                    {
                        ModuleName = m.ModuleName,
                        FileName = m.FileName,
                        MemorySizeKB = (m.ModuleMemorySize / 1024).ToString("N0") + " KB",
                        BaseAddress = "0x" + m.BaseAddress.ToString("X")
                    });
                }
            }
            catch { }
            lvModules.ItemsSource = modules;
        }

        private void MenuInfo_Click(object sender, RoutedEventArgs e)
        {
            ProcessViewModel sel = lvProcesses.SelectedItem as ProcessViewModel;
            if (sel != null)
            {
                tabInfo.SelectedIndex = 0;
                ShowProcessInfo(sel.Id);
            }
        }

        private void MenuThreads_Click(object sender, RoutedEventArgs e)
        {
            ProcessViewModel sel = lvProcesses.SelectedItem as ProcessViewModel;
            if (sel != null)
            {
                ShowProcessInfo(sel.Id);
                tabInfo.SelectedIndex = 1;
            }
        }

        private void MenuModules_Click(object sender, RoutedEventArgs e)
        {
            ProcessViewModel sel = lvProcesses.SelectedItem as ProcessViewModel;
            if (sel != null)
            {
                ShowProcessInfo(sel.Id);
                tabInfo.SelectedIndex = 2;
            }
        }

        private void BtnKill_Click(object sender, RoutedEventArgs e)
        {
            ProcessViewModel sel = lvProcesses.SelectedItem as ProcessViewModel;
            if (sel == null)
            {
                MessageBox.Show("Спочатку виберіть процес зі списку.",
                                "Нічого не вибрано",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Зупинити процес «" + sel.ProcessName + "» (PID " + sel.Id + ")?",
                "Підтвердження",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                Process.GetProcessById(sel.Id).Kill();
                txtStatus.Text = "Процес " + sel.ProcessName + " (PID " + sel.Id + ") зупинено.";
                LoadProcesses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося зупинити процес:\n" + ex.Message,
                                "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FileName = "processes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            dlg.DefaultExt = ".txt";

            if (dlg.ShowDialog() != true) return;

            try
            {
                using (StreamWriter writer = new StreamWriter(dlg.FileName, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("Список процесів  —  " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                    writer.WriteLine(new string('-', 100));
                    writer.WriteLine(
                        string.Format("{0,-8} {1,-30} {2,-16} {3,-12} {4,-8} {5,-22} {6}",
                        "ID", "Назва", "Пам'ять (KB)", "Вірт (MB)", "Потоки", "Час запуску", "Модуль"));
                    writer.WriteLine(new string('-', 100));

                    foreach (ProcessViewModel p in _allProcesses)
                    {
                        writer.WriteLine(
                            string.Format("{0,-8} {1,-30} {2,-16} {3,-12} {4,-8} {5,-22} {6}",
                            p.Id, p.ProcessName, p.MemoryKB, p.VirtualMemoryMB,
                            p.ThreadCount, p.StartTime, p.MainModule));
                    }

                    writer.WriteLine(new string('-', 100));
                    writer.WriteLine("Всього: " + _allProcesses.Count + " процесів");
                }

                txtStatus.Text = "Список збережено: " + dlg.FileName;
                MessageBox.Show("Файл успішно збережено:\n" + dlg.FileName,
                                "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збереженні:\n" + ex.Message,
                                "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
