namespace Messages
{
    public enum HttpResultCode
    {
        Ok = 200,
        Created = 201,
        Accepted = 203,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        Locked = 423,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        InternalServerError = 500,
        NotImplemented = 501
    }
}
