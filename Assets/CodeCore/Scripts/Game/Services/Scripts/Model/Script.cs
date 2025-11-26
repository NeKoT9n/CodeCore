namespace Assets.CodeCore.Scripts.Game.Services.Scripts.Model
{
    public class Script
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        public Script(string name)
        {
            Name = name;
        }

        public void SetCode(string code)
        {
            Code = code;
        }
    }
}
