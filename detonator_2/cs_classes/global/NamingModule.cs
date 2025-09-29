using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class NamingModule : Node
{
    public readonly Dictionary<String, Dictionary<String, Array<String>>> NAME_VARIANTS = new Dictionary<string, Dictionary<String, Array<String>>>
    {
        {"korean", new Dictionary<String, Array<String>>{
                {"first_name", [
                    //"O"
                    "Onji",
                    // "H"
                    "Haneul"
                ]
                },
                {"last_name", [
                    //"G"
                    "Gang",
                    //"R"
                    "Ryu",
                ]}
            }
        },
        { "japanese",  new Dictionary<String, Array<String>>{
            {"first_name", [
                //"M"
                "Minene",
                //"Y"
                "Yuno",
                "Yukiteru",
            ]
            },
            {"last_name", [
                //"A"
                "Amano",
                //"G"
                "Gasai",
                //"U"
                "Uryuu",
            ] }
        }
        },
        { "etc" , new Dictionary<String, Array<String>>{
            {"first_name", []},
            {"last_name", []}
        } },

    };


    public Dictionary<String, Array<String>> name_data = new Dictionary<String, Array<String>>();
    public Dictionary<String, Array<String>> using_names = new Dictionary<String, Array<String>>();

    public (String last_name, String first_name) get_random_name_from_defualt_variant(String faction)
    {
        (String last_name, String first_name) result;
        String country = "";

        Array<String> factions = ["korean", "japanese", "etc"];

        country = (!factions.Contains(faction)) ? factions.PickRandom() : faction;

        Dictionary<String, Array<String>> ll_variants = NAME_VARIANTS[country];

        String last_name = ll_variants["last_name"].PickRandom();
        Array<String> fl_variants = NAME_VARIANTS[country]["first_name"];

        if (using_names.Count != 0)
        {
            if (using_names.ContainsKey(last_name))
            {
                Array<String> using_first_names = using_names[last_name];
                foreach (String name in using_first_names)
                {
                    if (fl_variants.Contains(name))
                    {
                        fl_variants.Remove(name);
                    }
                }
            }
        }

        String first_name = fl_variants.PickRandom();

        if (using_names.ContainsKey(last_name))
        {
            using_names[last_name].Add(first_name);
        }
        else
        {
            using_names.Add(last_name, [first_name]);
        }

        result = (last_name, first_name);

        return result;

    }

}
