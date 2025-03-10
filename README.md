# ChatBotAI

1. How to run the application
   - install docker
   - in the root folder (place where docker-compose file exist) run command "docker-compose up"
   - navigate to http://localhost:4201/

2. Description
	  
	The application allows you to talk to a bot in real time, view the history of the conversation and rate its responses. While the bot is generating a response, it is possible to cancel this operation, so that only the text generated up to that point will be saved in the history, and the bot itself will stop generating a response.

	Backend side was written in Clean Architecture using technologies including ASP.NET Core, EF Core, SQL Server and MediatR. SSE (Server Sent Events) was used for live communication. Frontend side was written in Angular 19, TypeScript, Tailwind CSS.

	The current bot model generates random responses using the NLipsum library. To substitute the implementation of the real bot, it is enough to implement the IGenerateChatAnswerService interface.

3. An example of a conversation with a bot:


![Screenshot 2025-03-10 011816](https://github.com/user-attachments/assets/d620da32-5f8e-4cb2-a3bd-23df092edaa3)



