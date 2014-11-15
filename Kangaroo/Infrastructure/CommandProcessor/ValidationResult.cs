//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Kangaroo.Infrastructure.CommandProcessor
//{
//     /// <summary>
//    /// Describes the result of a validation of a potential change through a business service.
//    /// </summary>
//    public class CommandValidationResult
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="CommandValidationResult"/> class.
//        /// </summary>
//        public CommandValidationResult()
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="CommandValidationResult"/> class.
//        /// </summary>
//        /// <param name="memeberName">Name of the memeber.</param>
//        /// <param name="message">The message.</param>
//        public CommandValidationResult(string memeberName, string message)
//        {
//            this.MemberName = memeberName;
//            this.Message = message;
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="CommandValidationResult"/> class.
//        /// </summary>
//        /// <param name="message">The message.</param>
//        public CommandValidationResult(string message)
//        {
//            this.Message = message;
//        }

//        /// <summary>
//        /// Gets or sets the name of the member.
//        /// </summary>
//        /// <value>
//        /// The name of the member.  May be null for general validation issues.
//        /// </value>
//        public string MemberName { get; set; }

//        /// <summary>
//        /// Gets or sets the message.
//        /// </summary>
//        /// <value>
//        /// The message.
//        /// </value>
//        public string Message { get; set; }

//        public static List<CommandValidationResult> Empty() {
//            return new List<CommandValidationResult>();
//        }
//    }
//}