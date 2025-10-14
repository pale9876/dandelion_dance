##### Extends Node
---

##### Hitbox에서 발생된 HitEvent들을 처리하는 객체입니다.

이벤트가 발생하면 Hitbox가 Event를 생성요청하여 다음 프레임에서 Unit의 다음 행동을 순차적으로 처리 및 적용합니다.

이벤트에 대한 처리는 Physics Tick Frame에 맞추어 진행됩니다. [[HitEvent]]에 대한 자세한 정보는 해당 항목을 참조하시길 바랍니다.