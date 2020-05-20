using System;
using UnityEngine;

public static class Colors
{
    public static Color Primary = GetColorFromString("00C9AE");
    public static Color Secondary = GetColorFromString("F87E59");


    /*------------------------------------------------------------------*\
    |*							TOOLS
    \*------------------------------------------------------------------*/

    public static string Dec_to_Hex(int value) => value.ToString("X2");
    public static int Hex_to_Dec(string hex) => Convert.ToInt32(hex, 16);
    public static string Dec01_to_Hex(float value) => Dec_to_Hex((int) Mathf.Round(value * 255f));
    public static float Hex_to_Dec01(string hex) => Hex_to_Dec(hex) / 255f;

    public static string GetStringFromColor(Color color) {
        string red = Dec01_to_Hex(color.r);
        string green = Dec01_to_Hex(color.g);
        string blue = Dec01_to_Hex(color.b);
        return red + green + blue;
    }

    public static string GetStringFromColorWithAlpha(Color color) {
        string alpha = Dec01_to_Hex(color.a);
        return GetStringFromColor(color) + alpha;
    }

    // Todo: Buggy, needs to be tested one day.
    public static void GetStringFromColor(Color color,
                                          out string red,
                                          out string green,
                                          out string blue,
                                          out string alpha) {
        red = Dec01_to_Hex(color.r);
        green = Dec01_to_Hex(color.g);
        blue = Dec01_to_Hex(color.b);
        alpha = Dec01_to_Hex(color.a);
    }

    public static string GetStringFromColor(float r, float g, float b) {
        string red = Dec01_to_Hex(r);
        string green = Dec01_to_Hex(g);
        string blue = Dec01_to_Hex(b);
        return red + green + blue;
    }

    public static string GetStringFromColor(float r, float g, float b, float a) {
        string alpha = Dec01_to_Hex(a);
        return GetStringFromColor(r, g, b) + alpha;
    }

    public static Color GetColorFromString(string color) {
        float red = Hex_to_Dec01(color.Substring(0, 2));
        float green = Hex_to_Dec01(color.Substring(2, 2));
        float blue = Hex_to_Dec01(color.Substring(4, 2));
        float alpha = 1f;

        if (color.Length >= 8) {
            // Color string contains alpha
            alpha = Hex_to_Dec01(color.Substring(6, 2));
        }
        return new Color(red, green, blue, alpha);
    }
}
