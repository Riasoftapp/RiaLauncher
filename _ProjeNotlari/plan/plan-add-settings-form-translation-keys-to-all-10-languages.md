# 🎯 Add Settings Form Translation Keys to All 10 Languages

## Understanding
User needs to add the Settings form translation keys (Settings, LaunchMode, ViewMode, Language, LastActiveTab, SelectLanguage, SelectTab, LaunchDoubleClick, LaunchSingleClick, ViewIconText, ViewIcon, ViewList, ViewTile, AlwaysOnTop, OK, Cancel) to all 10 language files (ar.lng, en.lng, es.lng, fr.lng, it.lng, ja.lng, ko.lng, la.lng, tr.lng, zh.lng).

## Assumptions
- All language files have the same structure with Copy/Move Form section at the end
- Settings keys should be added after the Copy/Move Form section
- Each language needs culturally appropriate translations
- Files are encoded in UTF-8

## Approach
For each of the remaining 8 language files (ar, es, fr, it, ja, ko, la, and we already did en, tr, zh), add the Settings Form section with appropriate translations. This follows the existing pattern of adding comments, key=value pairs for UI elements.

## Key Files
- assets/lang/ar.lng (Arabic)
- assets/lang/es.lng (Spanish)
- assets/lang/fr.lng (French)
- assets/lang/it.lng (Italian)
- assets/lang/ja.lng (Japanese)
- assets/lang/ko.lng (Korean)
- assets/lang/la.lng (Latin)

## Risks & Open Questions
- Latin (la.lng) may not be a practical UI language but needs entries anyway
- Need to ensure all 16 settings keys are present in each file

**Last Updated**: 2026-07-25 04:36:55

## 📝 Plan Steps
-  **Add Settings keys to it.lng (Italian)**
-  **Add Settings keys to es.lng (Spanish)**
-  **Add Settings keys to fr.lng (French)**
-  **Add Settings keys to ar.lng (Arabic)**
-  **Add Settings keys to ja.lng (Japanese)**
-  **Add Settings keys to ko.lng (Korean)**
-  **Add Settings keys to la.lng (Latin)**
-  **Build and verify no errors**

