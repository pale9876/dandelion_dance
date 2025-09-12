#include "register_types.h"

//#include "gdexample.h"
#include "entity.h"
#include "nemesis_system.h"


#include <gdextension_interface.h>
#include <godot_cpp/core/defs.hpp>
#include <godot_cpp/godot.hpp>
#include <godot_cpp/classes/engine.hpp>


using namespace godot;

void initialize_mp_module(ModuleInitializationLevel p_level) {
    if (p_level != MODULE_INITIALIZATION_LEVEL_SCENE) {
        return;
    }

    GDREGISTER_RUNTIME_CLASS(Entity);
    GDREGISTER_CLASS(NemesisSystem);

    NemesisSystem::init_singleton();

    Engine* engine = Engine::get_singleton();
    engine -> register_singleton("NemesisSystem", &NemesisSystem::get_sys());
}

void uninitialize_mp_module(ModuleInitializationLevel p_level) {
    if (p_level != MODULE_INITIALIZATION_LEVEL_SCENE) {
        return;
    }

    Engine* engine = Engine::get_singleton();
    engine -> unregister_singleton("NemesisSystem");

    NemesisSystem::uninit();
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