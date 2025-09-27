using Godot;
using System;
using KoreanTyper;
using sex = KoreanTyper.stringExtensions;

using Godot.Collections;
using System.Linq;

[GlobalClass]
public partial class KoreanTypingEffecter : RefCounted
{

    String str = "";
    String current_value = "";
    public Array<String> chars = [];
    int arr_idx = -1;
    int sub_max_idx = -1;
    int sub_idx = -1;
    int index = -1;
    int max_index = -1;

    public KoreanTypingEffecter(String text)
    {
        this.str = text;

        if (this.str.Count() == 0)
        {
            GD.PrintErr("입력된 문자열이 없습니다.");
        }
        else
        {
            foreach (char c in str)
            {
                var elem = c.ToString();
                chars.Add(elem);
            }
        }

        this.max_index = chars.Count - 1;
    }

    public static KoreanTypingEffecter create(String value)
    {
        return new KoreanTypingEffecter(value);
    }

    public String get_next()
    {
        if (index == 0)
        {
            arr_idx += 1;
            index += 1;
            sub_max_idx = sex.get_typed_length(chars[arr_idx].ToString());
            sub_idx += 1;
        }

        return "";
    }

    public void jump_to(int idx)
    {

    }

    public Array<int> divide(bool print = false)
    {
        Array<int> result = new Array<int>();

        if (!(chars.Count == 0))
        {
            foreach (string s in chars)
            {
                result.Add(sex.get_typed_length(s));
            }
        }

        if (print)
            GD.Print(result/*Debug*/);

        return result;
    }

    // static methods
    public static String get_typing(String str, int count)
    {
        return stringExtensions.Typing(str, count);
    }

    public Array<String> get_chars()
    {
        Array<String> result = new Array<String>();

        foreach (String s in chars)
        {
            result.Add(s);
        }

        return result;
    }

}
