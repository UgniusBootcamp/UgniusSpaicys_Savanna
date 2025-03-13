import {
  HttpTransportType,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';

class SignalRService {
  constructor() {
    this.connection = null;
  }

  async startConnection(url) {
    const token = localStorage.getItem('accessToken');
    console.log(url);

    this.connection = new HubConnectionBuilder()
      .withUrl(url, {
        accessTokenFactory: () => token,
        transport: HttpTransportType.WebSockets | HttpTransportType.LongPolling,
      })
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    try {
      await this.connection.start();
    } catch (err) {
      console.error('Error while starting SignalR connection:', err);
    }
  }

  async stopConnection() {
    if (this.connection) {
      await this.connection.stop();
    }
  }

  async invokeCreateGame(gameCreateDto) {
    if (this.connection) {
      try {
        await this.connection.invoke('CreateGame', gameCreateDto);
      } catch (err) {
        console.error('Error invoking CreateGame method:', err);
      }
    }
  }

  async invokeCreateAnimal(animalTypeId) {
    if (this.connection) {
      try {
        await this.connection.invoke('CreateAnimal', animalTypeId);
      } catch (err) {
        console.error('Error invoking CreateAnimal method:', err);
      }
    }
  }
}

export default new SignalRService();
