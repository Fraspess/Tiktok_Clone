# Contributing Guide

## Гілки (Branches)

| Гілка | Призначення |
|---|---|
| `main` | Стабільна версія. Тільки перевірений код. |
| `develop` | Основна гілка розробки. Сюди мержиться все. |
| `feature/...` | Твоя особиста гілка для задачі. |

> 🔁 Схема руху коду: `feature` → `develop` → `main`

**Прямі коміти в `main` і `develop` — заборонені.**

### Як назвати гілку:
```
feature/auth-jwt
feature/video-upload
fix/login-redirect
```

---

## Як синхронізуватись з develop

Якщо в `develop` з'явились нові коміти — **не чекай**, оновлюй свою гілку:

```bash
git checkout develop
git pull origin develop
git checkout твоя-гілка
git merge develop
```

> ⚠️ Якщо ти відстаєш від `develop` на 3+ коміти — злий `develop` у свою гілку **одразу**.
> Не чекай поки накопичиться 20 конфліктів.

---

## Pull Request

1. Переконайся що твій код **не ламає те що вже працює**
2. Створи PR з `твоя-гілка` → `develop`
3. Назва PR: коротко що зроблено, наприклад: `Add JWT auth`, `Fix video upload`
4. Призначи **reviewer** — когось з команди
5. Тільки після апруву — мерджиш

> 🚀 З `develop` в `main` мержить **тільки тімлід** після фінального тестування.

---

## Коміти

Пиши зрозумілі коміт-меседжі:

```
✅ Add JWT token generation
✅ Fix login form validation
✅ Remove unused imports

❌ fix
❌ asdfgh
❌ changes
❌ test123
```

---

## Заборонено

- ❌ Пушити напряму в `main`
- ❌ Мерджити без PR
- ❌ Ігнорувати конфлікти і пушити через force
- ❌ Коміти типу `fix`, `test`, `aaa`

---

## Коротко — алгоритм роботи

```
1. Стягнув develop → створив гілку → зробив задачу
2. Регулярно мерджиш develop у свою гілку (кожні 2-3 коміти в develop)
3. Готово → PR в develop → review → merge
4. В main мержить тільки тімлід
```
