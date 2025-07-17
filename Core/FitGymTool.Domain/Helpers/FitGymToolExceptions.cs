// *********************************************************************************
//	<copyright file="FitGymToolExceptions.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The FitGym Tool Business Exceptions.</summary>
// *********************************************************************************

using System.Diagnostics.CodeAnalysis;

namespace FitGymTool.Domain.Helpers;

/// <summary>
/// The FitGym Tool Business Exceptions.
/// </summary>
[ExcludeFromCodeCoverage]
public class FitGymToolExceptions : Exception
{
	/// <summary>
	/// Gets or sets the status code.
	/// </summary>
	/// <value>
	/// The status code.
	/// </value>
	public int StatusCode { get; set; }

	/// <summary>
	/// Gets or sets the message.
	/// </summary>
	/// <value>
	/// The message.
	/// </value>
	public string? ExceptionMessage { get; set; }

	/// <summary>
	/// Gets or sets the details.
	/// </summary>
	/// <value>
	/// The details.
	/// </value>
	public string? Details { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="FitGymToolExceptions"/> class.
	/// </summary>
	/// <param name="statusCode">The status code.</param>
	/// <param name="message">The message.</param>
	/// <param name="details">The details.</param>
	public FitGymToolExceptions(string? message) : base(message)
	{
		this.ExceptionMessage = message;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="FitGymToolExceptions"/> class.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="statusCode">The status code.</param>
	/// <param name="details">The details.</param>
	public FitGymToolExceptions(string? message, int statusCode, string? details) : base(message)
	{
		this.ExceptionMessage = message;
		this.StatusCode = statusCode;
		this.Details = details;
	}
}
