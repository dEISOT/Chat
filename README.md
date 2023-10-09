# ShuttleX

### :zap: **Description**

Test task for ShuttleX. Web Application built with C#,
which provides functionality for chating between users  

### :white_check_mark: **The stack of technologies used**
:star: **API Technologies:**
- SOLID
- OOP
- DI
- REST

:desktop_computer: **Backend technologies:**
- ASP.NET Core 6
- Entity Framework Core
- SignalR
- Automapper
- FluentAPI

:floppy_disk: **DataBase:**
- SQLServer

:heavy_check_mark: **Testing:**
- XUnit

### :pushpin: **API Endpoints**

**Remember to put User-Id in request header**

**Users**
  
| **HTTP METHOD** |         **ENDPOINT**         |         **DESCRIPTION**        |
|:---------------:|:----------------------------:|:------------------------------:|
|    **GET**      |       `/api/v1/users/`       |          Get all users         |
|    **POST**     |       `/api/v1/users/`       |          Add new user          |

**Message**

| **HTTP METHOD** |                    **ENDPOINT**                |            **DESCRIPTION**           |
|:---------------:|:----------------------------------------------:|:------------------------------------:|
|     **GET**     |`api/v1/messages/conversations/{conversationId}`|    Get all messages in conversation  |

**Members**
  
| **HTTP METHOD** |                  **ENDPOINT**                  |         **DESCRIPTION**        |
|:---------------:|:----------------------------------------------:|:------------------------------:|
|    **POST**     |`/api/v1/members/conversations/{conversationId}`|     Add users in conversation  |
|    **DELETE**   |`/api/v1/members/conversations/{conversationId}`|   Delete user in conversation  |

**Conversations**
  
| **HTTP METHOD** |                  **ENDPOINT**                  |         **DESCRIPTION**        |
|:---------------:|:----------------------------------------------:|:------------------------------:|
|     **GET**     |            `/api/v1/conversations/`            |      Get all conversations     |
|     **GET**     |    `/api/v1/conversations/{conversationId}`    |      Get conversation by Id    |
|     **GET**     |      `/api/v1/conversations/search/{name}`     |     Get conversation by name   |
|    **POST**     |         `/api/v1/conversations/create`         | Create conversation with users |
|   **DELETE**    |    `/api/v1/conversations/{conversationId}`    |       Delete conversation      |
