using System;
namespace BigBrother.LanguageUtils
{
    public interface ILanguageUtil
    {
        string Language { get; }
        string GetClrType(string keyword);
        string GuessResultFieldName();
    }
}
