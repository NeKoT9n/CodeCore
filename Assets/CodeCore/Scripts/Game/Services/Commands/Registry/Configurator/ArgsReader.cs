using System;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public readonly struct ArgsReader
    {
        private readonly string _commandName;
        private readonly object[] _args;

        public ArgsReader(string commandName, object[] args)
        {
            _commandName = commandName;
            _args = args;
        }

        public int Count => _args.Length;

        public T Read<T>(int index)
        {
            if (index >= _args.Length)
            {
                throw new ArgumentException(
                    $"Command '{_commandName}': Missing argument at position {index + 1}. Expected at least {index + 1}, got {_args.Length}.");
            }

            object arg = _args[index];
            Type targetType = typeof(T);
            Type actualType = arg.GetType();

            if (IsNarrowingConversion(actualType, targetType, arg))
            {
                throw new ArgumentException(
                    $"Command '{_commandName}': Argument {index + 1} requires type '{targetType.Name}', " +
                    $"but the provided value '{arg}'.");
            }
           
            try
            {
    
                return (T)Convert.ChangeType(arg, targetType, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Command '{_commandName}': Argument {index + 1} (value: '{arg}') could not be converted " +
                    $"to type '{targetType.Name}'. Details: {ex.Message}"
                );
            }
        }

        public void ValidateCount(int expected)
        {
            if (_args.Length != expected)
                throw new ArgumentException($"Command '{_commandName}' expects exactly {expected} argument(s), but got {_args.Length}.");
        }

        private bool IsNarrowingConversion(Type actualType, Type targetType, object value)
        {
           
            bool isActualFloat = actualType == typeof(float) || actualType == typeof(double);
           
            bool isTargetInt = targetType == typeof(int) || targetType == typeof(long) || targetType == typeof(short);

            if (isActualFloat && isTargetInt)
            {     
                double dValue = Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture);
      
                return Math.Abs(dValue - Math.Floor(dValue)) > double.Epsilon;
            }
       
            return false;
        }
    }
}
