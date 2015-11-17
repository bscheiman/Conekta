#region
using System;
using System.Linq.Expressions;
using System.Text;
using Conekta.Objects;

#endregion

namespace Conekta.Helpers {
    internal static class ExpressionHelper {
        internal static string GetBinaryExpression(BinaryExpression body) {
            if (body.Left is BinaryExpression) {
                return string.Format("{0}{1}", GetBinaryExpression(body.Left as BinaryExpression),
                    GetBinaryExpression(body.Right as BinaryExpression));
            }

            if (body.Left is MemberExpression && body.Right is ConstantExpression) {
                string name = (body.Left as MemberExpression).Member.Name.ToLower();
                var val = (body.Right as ConstantExpression).Value;

                return string.Format("{0}{1}={2}&", name, GetOperator(body), val);
            }

            return "";
        }

        internal static string GetOperator(BinaryExpression body) {
            switch (body.NodeType) {
                case ExpressionType.GreaterThan:
                    return ".gt";

                case ExpressionType.GreaterThanOrEqual:
                    return ".gte";

                case ExpressionType.Equal:
                    return "";

                case ExpressionType.LessThan:
                    return ".lt";

                case ExpressionType.LessThanOrEqual:
                    return ".lte";

                case ExpressionType.NotEqual:
                    return ".ne";
            }

            throw new InvalidOperationException();
        }

        internal static void GetQuery(Expression<Func<Charge, bool>> tree, ref StringBuilder sb) {
            if (tree.Body is BinaryExpression)
                sb.AppendFormat(GetBinaryExpression(tree.Body as BinaryExpression));
            else {
                Console.WriteLine(tree);
                return;

                Console.WriteLine("Paramter Count: {0}", tree.Parameters.Count);

                foreach (var param in tree.Parameters)
                    Console.WriteLine("\tParameter Name: {0}", param.Name);

                var body = (BinaryExpression) tree.Body;
                sb.AppendFormat(GetBinaryExpression(body));

                Console.WriteLine("Binary Expression Type: {0}", body.NodeType);
                Console.WriteLine("Method to be called: {0}", body.Method);
                Console.WriteLine("Return Type: {0}", tree.ReturnType);

                Console.WriteLine(sb.ToString());
            }
        }
    }
}
