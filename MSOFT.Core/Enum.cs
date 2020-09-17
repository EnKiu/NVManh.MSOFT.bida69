using System;

namespace MSOFT.Core.Enum
{
    public enum ProcdureTypeName
    {
        /// <summary>
        ///  Lấy dữ liệu
        /// </summary>
        Get,

        /// <summary>
        /// Lấy dữ liệu theo khóa chính
        /// </summary>
        GetById,

        /// <summary>
        /// Thêm mới
        /// </summary>
        Insert,

        /// <summary>
        /// Sửa/ cập nhật dữ liệu
        /// </summary>
        Update,

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        Delete,

        /// <summary>
        /// Lấy dữ liệu có phân trang
        /// </summary>
        GetPaging
    }
}
