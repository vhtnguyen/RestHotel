namespace Hotel.Shared.Authentication;

internal class JsonWebToken
{
}

/*
 
    khi user request resource có chứa quyền
    request kèm token
    
    // header
    - thông tin token header bao gồm: 
    // payload
    - thông tin token bao gồm: userId, username, roles

    - extract token ra
        - find userId trong bảng user ứng với token đó
        - add role claim vào cho user đó
        - add 1 vài permission vào cho user đó
*/
