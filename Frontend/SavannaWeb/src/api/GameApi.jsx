import endpointConstants from '../constants/endpointConstants';
import api from './Api';

const GameApi = {
  async getAnimalTypes() {
    return api.get(endpointConstants.animalTypes);
  },
  async getUserGames(start, end) {
    const params = new URLSearchParams();

    params.append(endpointConstants.start, start);
    params.append(endpointConstants.end, end);

    return api.get(`${endpointConstants.games}${params.toString()}`);
  },
};

export default GameApi;
