using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYM_MVC.Core.Helper {

    public static class EnumHelper {

        public static SelectList ToSelectList<TEnum>() where TEnum : struct, Enum {
            return new SelectList(Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new { Value = e, Text = e.ToString() }), "Value", "Text");
        }

        public static TEnum ToEnum<TEnum>(string value) where TEnum : struct, Enum {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }
}