using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Excel.Util.ExcelTemplate
{
    public class PropertyMapping<TModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="propertyExpression"></param>
        public PropertyMapping(int columnIndex, Expression propertyExpression)
        {
            ColumnIndex = columnIndex;
            PropertyExpression = propertyExpression;
        }

        /// <summary>
        /// 映射的列号
        /// </summary>
        public int ColumnIndex { get; private set; }

        /// <summary>
        /// 属性表达式
        /// </summary>
        public Expression PropertyExpression { get; private set; }
    }
}
