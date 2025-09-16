#include "register_types.h"

#include "entity.h"
#include "nemesis_system.h"
#include "squad.h"
#include "auto_sprite.h"
#include "auto_sprite_component.h"
#include "executioner.h"
#include "cyp_event.h"

#include <gdextension_interface.h>
#include <godot_cpp/core/defs.hpp>
#include <godot_cpp/godot.hpp>
#include <godot_cpp/classes/engine.hpp>


using namespace godot;


void initialize_mp_module(ModuleInitializationLevel p_level) {
    if (p_level != MODULE_INITIALIZATION_LEVEL_SCENE) {
        return;
    }


    GDREGISTER_VIRTUAL_CLASS(CypEvent);
    GDREGISTER_CLASS(Entity);
    GDREGISTER_CLASS(Squad);
    GDREGISTER_CLASS(NemesisSystem);
    GDREGISTER_CLASS(Executioner)

    GDREGISTER_CLASS(AutoSprite)
    GDREGISTER_CLASS(AutoSpriteComponent);

    // Autoload init
    NemesisSystem::init_singleton();
    Executioner::exec_init();

    Engine* engine = Engine::get_singleton();
    engine -> register_singleton("NemesisSystem", &NemesisSystem::get_sys());
    engine -> register_singleton("Executioner", &Executioner::get_sys());

}

void uninitialize_mp_module(ModuleInitializationLevel p_level) {
    if (p_level != MODULE_INITIALIZATION_LEVEL_SCENE) {
        return;
    }


    // Autoload deinit
    Engine* engine = Engine::get_singleton();
    engine -> unregister_singleton("NemesisSystem");
    engine -> unregister_singleton("Executioner");

    NemesisSystem::uninit();
    Executioner::deinit();
}

extern "C" {
    // Initialization.
    GDExtensionBool GDE_EXPORT mp_module_init(GDExtensionInterfaceGetProcAddress p_get_proc_address, const GDExtensionClassLibraryPtr p_library, GDExtensionInitialization* r_initialization) {
        godot::GDExtensionBinding::InitObject init_obj(p_get_proc_address, p_library, r_initialization);

        init_obj.register_initializer(initialize_mp_module);
        init_obj.register_terminator(uninitialize_mp_module);
        init_obj.set_minimum_library_initialization_level(MODULE_INITIALIZATION_LEVEL_SCENE);

        return init_obj.init();
    }
}