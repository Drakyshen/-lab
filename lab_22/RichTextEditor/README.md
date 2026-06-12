# Лабораторна робота №22 — RTF Editor (WPF, .NET 8)

## Виконані вимоги

### 90–100 балів (повний варіант)
- ✅ Багатовіконний текстовий редактор (Ctrl+Shift+N — нове вікно)
- ✅ Завантаження та збереження RTF-документів (Open / Save / Save As)
- ✅ Керування жирним, курсивом, підкресленням (Ctrl+B / I / U)
- ✅ Вирівнювання тексту: ліво / центр / право / по ширині
- ✅ Два ComboBox: сімейство шрифтів + розмір (системні шрифти)
- ✅ Відновлення стану кнопок при зміні виділення (SelectionChanged)
- ✅ **Кілька мов інтерфейсу** — Українська / English (перемикання в меню)
- ✅ **Вставка зображень** в документ (Insert → Image…)
- ✅ Вибір кольору тексту (ColorDialog)
- ✅ Статусний рядок
- ✅ Підтвердження закриття при незбережених змінах
- ✅ WPF команди (ApplicationCommands + EditingCommands)
- ✅ Keyboard shortcuts: Ctrl+N, Ctrl+O, Ctrl+S, Ctrl+Shift+S, Ctrl+B/I/U

---

## Структура проєкту

```
RichTextEditor/
├── RichTextEditor.csproj
├── App.xaml / App.xaml.cs
├── MainWindow.xaml / MainWindow.xaml.cs   ← startup, opens first editor
├── EditorWindow.xaml                       ← головне вікно редактора (UI)
├── EditorWindow.xaml.cs                    ← code-behind (вся логіка)
├── EditorCommands.cs                       ← custom RoutedUICommands
├── WindowManager.cs                        ← менеджер вікон
├── Localization/
│   └── LocalizationManager.cs             ← UA / EN рядки
└── Themes/
    └── DefaultTheme.xaml                  ← стилі (кнопки, комбо, RTB)
```

---

## Збірка та запуск

### Вимоги
- Windows 10/11
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 **або** CLI

### Через Visual Studio 2022
1. Відкрийте `RichTextEditor.csproj`
2. Натисніть **F5** або **Ctrl+F5**

### Через командний рядок (Windows)
```bat
cd RichTextEditor
dotnet build
dotnet run
```

---

## Архітектура

### Локалізація
`LocalizationManager` — singleton із властивостями-рядками для кожного
елемента UI. Зміна `Language` нотифікує WPF Binding через `INotifyPropertyChanged`,
тому весь інтерфейс оновлюється миттєво без перезапуску.

### Багатовіконність
`WindowManager` зберігає список всіх відкритих `EditorWindow`. Кожне вікно
незалежне (окремий `RichTextBox` + власний `_filePath`). При закритті
останнього вікна відбувається `Application.Shutdown()`.

### Вставка зображень
Обраний файл зображення завантажується як `BitmapImage`, масштабується
до макс. 600 px ширини і вставляється через `InlineUIContainer` у позицію
курсора `RichTextBox`.

### Стан кнопок
Метод `rtbEditor_SelectionChanged` читає властивості виділення через
`GetPropertyValue()` і встановлює `IsChecked` для ToggleButton'ів та
`SelectedItem`/`Text` для ComboBox'ів. Прапор `_suppressSelectionUpdate`
запобігає циклічним подіям.
