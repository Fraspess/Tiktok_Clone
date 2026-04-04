# Посібник із встановлення FFmpeg

FFmpeg — це потужний мультимедійний фреймворк для роботи з відео, аудіо та іншими медіафайлами. У цьому посібнику описано два способи встановлення на Windows.

---

## Зміст

- [Спосіб 1: Встановлення через Chocolatey](#спосіб-1-встановлення-через-chocolatey)
- [Спосіб 2: Ручне встановлення](#спосіб-2-ручне-встановлення)
- [Перевірка встановлення](#перевірка-встановлення)

---

## Спосіб 1: Встановлення через Chocolatey

[Chocolatey](https://chocolatey.org/) — це менеджер пакетів для Windows, який спрощує встановлення програм.

### Крок 1 — Встановити Chocolatey

Якщо Chocolatey ще не встановлено, відкрийте **PowerShell від імені адміністратора** та виконайте:

```powershell
Set-ExecutionPolicy Bypass -Scope Process -Force; `
[System.Net.ServicePointManager]::SecurityProtocol = `
[System.Net.ServicePointManager]::SecurityProtocol -bor 3072; `
iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
```

### Крок 2 — Встановити FFmpeg

Після встановлення Chocolatey виконайте:

```powershell
choco install ffmpeg
```

Щоб встановити конкретну версію:

```powershell
choco install ffmpeg --version=7.1
```

Щоб оновити FFmpeg у майбутньому:

```powershell
choco upgrade ffmpeg
```

> **Примітка:** Chocolatey автоматично додає FFmpeg до системного `PATH` — додаткове налаштування не потрібне.

---

## Спосіб 2: Ручне встановлення

### Крок 1 — Завантажити FFmpeg

1. Перейдіть на офіційну сторінку завантаження: [https://ffmpeg.org/download.html](https://ffmpeg.org/download.html)
2. У розділі **Windows** натисніть на одного з постачальників збірок (наприклад, **gyan.dev** або **BtbN**)
3. Завантажте останню **release**-збірку (наприклад, `ffmpeg-release-essentials.zip`)

### Крок 2 — Розпакувати архів

1. Клацніть правою кнопкою миші на завантаженому `.zip`-файлі та оберіть **Розпакувати все**
2. Перемістіть розпаковану папку у зручне постійне місце, наприклад:
   ```
   C:\ffmpeg
   ```

### Крок 3 — Додати FFmpeg до системного PATH

1. Відкрийте **Пуск** і знайдіть **Змінні середовища**
2. Натисніть **Змінити системні змінні середовища**
3. У вікні **Властивості системи** натисніть **Змінні середовища...**
4. У розділі **Системні змінні** знайдіть і виберіть **Path**, потім натисніть **Змінити...**
5. Натисніть **Створити** і додайте шлях до папки `bin`:
   ```
   C:\ffmpeg\bin
   ```
6. Натисніть **ОК** у всіх вікнах для збереження

---

## Перевірка встановлення

Відкрийте новий **Командний рядок** або **PowerShell** і виконайте:

```bash
ffmpeg -version
```

Ви маєте побачити щось подібне:

```
ffmpeg version 7.x.x Copyright (c) 2000-2024 the FFmpeg developers
built with gcc ...
```

---

## Корисні посилання

- 📄 [Офіційна документація FFmpeg](https://ffmpeg.org/documentation.html)
