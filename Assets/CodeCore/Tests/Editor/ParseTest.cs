using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States;
using Assets.CodeCore.Scripts.Game.Services.Scripts.Model;
using NUnit.Framework;
using System.Collections.Generic;

public class ParseTest
{
    private ScriptParser _parser;

    [SetUp]
    public void Setup()
    {
        _parser = new ScriptParser();
    }

    [Test]
    public void Parse_ValidSingleCommand_ReturnsCorrectResult()
    {
        var script = new Script("Player");
        script.SetCode("Move(10, 20);");

        List<ParseResult> results = _parser.Parse(script);

        var resultLine = results[0];

        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("Move", resultLine.CommandName);
        Assert.AreEqual(2, resultLine.Args.Length);
        Assert.AreEqual(10, resultLine.Args[0]);
        Assert.AreEqual(20, results[0].Args[1]);
        Assert.AreEqual(null, resultLine.Error);
      
    }

    [Test]
    public void Parse_MultipleCommands_ReturnsSeparateResults()
    {
        var script = new Script("Player");
        script.SetCode(@"Move(5, 10);
                        Wait(2);
                        Say(""Hello"");");


        List<ParseResult> results = _parser.Parse(script);

        Assert.AreEqual(3, results.Count);

        Assert.AreEqual("Move", results[0].CommandName);
        Assert.AreEqual("Wait", results[1].CommandName);
        Assert.AreEqual("Say", results[2].CommandName);
    }

    [Test]
    public void Parse_StringArgument_ParsedCorrectly()
    {
        var script = new Script("Player");
            script.SetCode("Say(\"Hi there\");");

        List<ParseResult> results = _parser.Parse(script);

        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("Say", results[0].CommandName);
        Assert.AreEqual("Hi there", results[0].Args[0]);
    }

    [Test]
    public void Parse_FloatArgument_ParsedCorrectly()
    {
        var script = new Script("Player");
        script.SetCode("Move(3.5);");

        List<ParseResult> results = _parser.Parse(script);

        Assert.AreEqual(1, results.Count);
        Assert.AreEqual(3.5f, results[0].Args[0]);
    }

    [Test]
    public void Parse_InvalidFormat_ReturnsErrorResult()
    {
        var script = new Script("Player");
        script.SetCode("Invalid Command");

        List<ParseResult> results = _parser.Parse(script);

        Assert.AreEqual(1, results.Count);
        Assert.IsTrue(results[0].IsFailure);
        StringAssert.Contains("Invalid command format", results[0].Error);
    }

    [Test]
    public void Parse_InvalidArgument_ReturnsErrorResult()
    {
        var script = new Script("Player");
        script.SetCode("Move(abc);");

        List<ParseResult> results = _parser.Parse(script);

        Assert.AreEqual(1, results.Count);
        Assert.IsTrue(results[0].IsFailure);
        StringAssert.Contains("Argument parsing failed", results[0].Error);
    }
 
}
