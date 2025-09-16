#pragma once

#ifndef NEMESIS_SYSTEM_H
#define NEMESIS_SYSTEM_H

#include "entity.h"

#include <godot_cpp/classes/node.hpp>
#include <godot_cpp/variant/typed_dictionary.hpp>
#include <godot_cpp/variant/dictionary.hpp>
#include <godot_cpp/classes/engine.hpp>

using namespace godot;

class NemesisSystem : public Node // Singleton
{
    GDCLASS(NemesisSystem, Node);

    public:
    static void init_singleton();
    static void uninit();
    static NemesisSystem &get_sys();
    static NemesisSystem* get_nemesis();

    TypedDictionary<String, PackedStringArray>
        first_name_variation = {};

    TypedDictionary<String, PackedStringArray>
        last_name_variation = {};

    void add_first_name_variation(const String& region, const String &first_name);
    void add_last_name_variation(const String& region, const String &last_name);

    String give_random_name(String faction);

    bool entity_entered(Node* entity);
    bool has_id(int id);
    bool has_node(Node* node);
    bool delete_entity(int id);


    TypedDictionary<int, Dictionary> get_entities() const;

    protected:
    static void _bind_methods();

    private:
    int index = 0;
    TypedDictionary<int, Dictionary> entities = {};
    

    TypedDictionary<String, PackedStringArray> get_first_names() const;
    void set_first_names(const TypedDictionary<String, PackedStringArray>& dict);
    
    TypedDictionary<String, PackedStringArray> get_last_names() const;
    void set_last_names(const TypedDictionary<String, PackedStringArray>& dict);

    int get_max_index() const;
    

};


#endif