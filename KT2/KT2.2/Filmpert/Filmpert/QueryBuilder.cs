using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmpert
{
    enum QueryOperators { EQUAL, NOTEQUAL, GREATER, LESS, GREATEROREQUAL, LESSOREQUAL, BETWEEN, LIKE, IN }
    enum AndOr { AND, OR }

    static class QueryBuilder
    {
        static public string BuildQuery(string table, string[] columns, QueryStatements statements)
        {
            string result = "";

            try
            {
                result += "Select ";

                for (int i = 0; i < columns.Length; i++)
                {
                    result += columns[i] + " ";
                }

                result += "FROM " + table + " WHERE ";

                for (int i = 0; i < statements.Columns.Length; i++)
                {
                    result += statements.Columns[i] + " " + statements.Operators[i];

                    int value;

                    if (!int.TryParse(statements.Values[i], out value))
                        result += " '" + statements.Values[i] + "' ";
                    else
                        result += " " + value + " ";

                }

                result = result.Trim() + ";";
            }
            catch (Exception)
            {
                
            }

            return result;
        }

        static public string BuildQuery(string table, string[] columns)
        {
            string result = "";

            try
            {
                result += "Select ";

                for (int i = 0; i < columns.Length; i++)
                {
                    result += columns[i] + " ";
                }

                result += "FROM " + table + ";";
            }
            catch (Exception)
            {
                
            }

            return result;
        }
    }

    struct QueryStatement
    {
        public string Column { get; private set; }
        public string Value { get; private set; }
        public QueryOperators Operator { get; private set; }

        public QueryStatement(string column, string value, QueryOperators qoperator)
        {
            Column = column;
            Value = value;
            Operator = qoperator;
        }
    }

    struct QueryStatements
    {
        public string[] Columns { get; private set; }
        public string[] Values { get; private set; }
        public QueryOperators[] Operators { get; private set; }
        public AndOr[] AndOrs { get; private set; }

        public QueryStatements(string[] columns, string[] values, QueryOperators[] operators, AndOr[] andOrs)
        {
            Columns = columns;
            Values = values;
            Operators = operators;
            AndOrs = andOrs;
        }
    }

}
