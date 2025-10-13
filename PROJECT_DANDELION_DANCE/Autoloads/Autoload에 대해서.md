
---

#### [See Docs](https://docs.godotengine.org/ko/4.x/tutorials/scripting/singletons_autoload.html)

![[singleton 1.webp]]

SceneTree 위에 위치하는 싱글톤 객체입니다. static 객체를 사용하여 정보를 저장하는 것보다 느리지만 데이터의 안정성과 Rust와 C++ GDExtension 확장언어와의 호환을 위하여 필요한 시스템입니다.

오토로드는 Root씬트리에서 메인루프보다 상위에 자리하게 되며 GetNode("/root/{AutoLoadName}")로 객체를 불러올 수 있습니다.