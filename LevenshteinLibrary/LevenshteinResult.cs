using System;
using System.Linq;
using System.Numerics;

namespace LevenshteinLibrary
{
    public class LevenshteinResult
    {
        private int[,] _matrix;

        private const char EqualCharacter = '=';
        private const char ChangeCharacter = 'o';
        private const char InsertionCharacter = '+';
        private const char DeletionCharacter = '-';

        public LevenshteinResult(string source, string target)
        {
            Source = (source ?? string.Empty).ToLower();
            Target = (target ?? string.Empty).ToLower();
            CalculateMatrix();
        }

        public int Distance { get; private set; }
        public string Source { get; }
        public string Target { get; }

        private void CalculateMatrix()
        {
            _matrix = new int[Source.Length + 1, Target.Length + 1];

            // Setup first row as all insertions
            for (var x = 0; x <= Source.Length; x++)
            {
                _matrix[x, 0] = x;
            }

            // Setup first column as all insertions
            for (var y = 0; y <= Target.Length; y++)
            {
                _matrix[0, y] = y;
            }

            // Fill in matrix
            for (var x = 1; x <= Source.Length; x++)
            {
                for (var y = 1; y <= Target.Length; y++)
                {
                    var match = Source[x - 1] == Target[y - 1];
                    var singleWildCard = Source[x - 1] == '?' || Target[y - 1] == '?';
                    var multipleWildCard = Source[x - 1] == '*' || Target[y - 1] == '*';

                    var substitutionCost = match || singleWildCard || multipleWildCard ? 0 : 1;
                    var deletionInsertionCost = multipleWildCard ? 0 : 1;

                    var deletion = _matrix[x - 1, y] + deletionInsertionCost;
                    var substitution = _matrix[x - 1, y - 1] + substitutionCost;
                    var insertion = _matrix[x, y - 1] + deletionInsertionCost;

                    _matrix[x, y] = new[]
                    {
                        deletion,
                        substitution,
                        insertion,
                    }.Min();
                }
            }

            Distance = _matrix[_matrix.GetUpperBound(0), _matrix.GetUpperBound(1)];
        }

        public override string ToString()
        {
            var path = GetPathPresentation();

            var sourceLine = string.Empty;
            var targetLine = string.Empty;

            var sourcePosition = 0;
            var targetPosition = 0;

            foreach (var t in path)
            {
                if (t == EqualCharacter || t == ChangeCharacter)
                {
                    sourceLine += Source[sourcePosition++];
                    targetLine += Target[targetPosition++];
                } 
                else if (t == InsertionCharacter)
                {
                    sourceLine += Source[sourcePosition++];
                    targetLine += " ";
                }
                else
                {
                    sourceLine += " ";
                    targetLine += Target[targetPosition++];
                }
            }


            return sourceLine + "\n" + path + "\n" + targetLine;
        }

        private string GetPathPresentation()
        {
            var result = string.Empty;

            var x = _matrix.GetUpperBound(0);
            var y = _matrix.GetUpperBound(1);

            while (x > 0 && y > 0)
            {
                var min = new[]
                {
                    _matrix[x - 1, y],
                    _matrix[x - 1, y - 1],
                    _matrix[x, y - 1]
                }.Min();

                var moveDiagonally = _matrix[x - 1, y - 1] == min;
                var moveUp = _matrix[x, y - 1] == min;

                if (moveDiagonally)
                {
                    // Substitution or equal
                    if (_matrix[x, y] == _matrix[x - 1, y - 1])
                    {
                        result = EqualCharacter + result;
                    }
                    else
                    {
                        result = ChangeCharacter + result;
                    }

                    x--;
                    y--;
                }
                else if (moveUp)
                {
                    result = DeletionCharacter + result;
                    y--;
                }
                else
                {
                    result = InsertionCharacter + result;
                    x--;
                }
            }

            return result;
        }
    }
}