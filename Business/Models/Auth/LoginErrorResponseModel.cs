﻿namespace Business.Models.Auth;

public class LoginErrorResponseModel
{
	public string Type { get; set; }
	public string Title { get; set; }
	public int Status { get; set; }
	public string TraceId { get; set; }
}
