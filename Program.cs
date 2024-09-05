// See https://aka.ms/new-console-template for more information
using BtlShare;
using Google.Protobuf;
using Newtonsoft.Json;

if (args.Length == 0)
{
    Console.WriteLine("没有输入指令。请使用 -help 查看可用指令。");
    return;
}

var command = args[0].ToLower();

if (command == "-all")
{
    string outPath = string.Empty;

    // 检查是否有 -outpath 参数
    if (args.Length > 1 && args[1].StartsWith("-outpath"))
    {
        outPath = GetArgumentValue(args[1]);
    }

    ExecuteAllCommand(outPath);
}
else if (command.StartsWith("-inputjson"))
{
    if (args.Length > 1 && args[1].StartsWith("-outputdata"))
    {
        string inputPath = GetArgumentValue(args[0]);
        string outputPath = GetArgumentValue(args[1]);
        ExecuteInputOutputCommand(inputPath, outputPath);
    }
    else
    {
        Console.WriteLine("缺少 -outputData 指令或格式错误。");
    }
}
else if (command == "-help")
{
    ShowHelp();
}
else
{ 
    Console.WriteLine("无效指令。请使用 -help 查看可用指令。");
}

static string GetArgumentValue(string arg)
{
    int index = arg.IndexOf('=');
    if (index >= 0)
    {
        return arg.Substring(index + 1).Trim('"');
    }
    return string.Empty;
}

static void ExecuteAllCommand(string outPath)
{
    Console.WriteLine("执行 -all 指令的逻辑...");
    
    Exporter.ExportAll2Json(outPath);

    // 在这里添加 -all 指令的逻辑，并使用 outPath 作为输出路径（如果提供）
}

static void ExecuteInputOutputCommand(string inputPath, string outputPath)
{
    Console.WriteLine($"执行 -inputjson 指令，输入路径：{inputPath}");
    Console.WriteLine($"执行 -outputdata 指令，输出路径：{outputPath}");
    // 在这里添加 -inputJson 和 -outputData 指令的逻辑
    var result = Exporter.InputJson2Data(inputPath, outputPath);

    if(result)
    {
        Console.WriteLine("执行成功");
    }
    else
    {
        Console.WriteLine("执行失败");
        ShowHelp();
    }
}

static void ShowHelp()
{
    Console.WriteLine("可用指令:");
    Console.WriteLine("-all [-outpath=\"path\"]: 导出全部data数据成json，并可选择指定输出路径。");
    Console.WriteLine("-inputjson=\"path\" -outputdata=\"path\": 指定输入 JSON 文件路径和输出数据文件路径。");
    Console.WriteLine("-help: 显示帮助信息。");
    Console.WriteLine("如遇问题联系作者：禽兽-云轩");
}