using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

public class IdentityException : Exception
{
    public IEnumerable<IdentityError> Errors { get; }

    public IdentityException(string message, IEnumerable<IdentityError> errors) : base(message)
    {
        catch (IdentityException ex)
        {
            var errorMessages = ex.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errorMessages });
        }
    }
}