using Godot;
using System;
using KoreanTyper;
using Godot.Collections;

public partial class KoreanTypingEffecter : RefCounted
{

    String str = "";
    String current_value = "";
    string[] chars;
    int arr_idx = -1;
    int sub_idx = -1;
    int index = -1;
    int max_index = -1;

    public KoreanTypingEffecter(String str)
    {
        this.str = str;
        max_index = stringExtensions.get_typed_length(str);

        chars = str.Split();
        if (chars.Length == 0)
        {
            GD.PrintErr("입력된 문자열이 없습니다.");
        }
        max_index = chars.Length - 1;
    }

    public String get_next() 
    {
        if (index == 0)
        {
            arr_idx += 1;
            index += 1;
            sub_idx += 1;
        }
        
        
        

        return "";
    }

    public void jump_to(int idx)
    {

    }

    public static String get_typing(String str, int count)
    {
        return stringExtensions.Typing(str, count);
    }

    
}
