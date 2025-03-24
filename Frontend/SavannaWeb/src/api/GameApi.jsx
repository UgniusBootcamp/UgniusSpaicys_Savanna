import endpointConstants from '../constants/endpointConstants';
import api from './Api';

const GameApi = {
  async getAnimalTypes() {
    return api.get(endpointConstants.animalTypes);
  },
  async getUserGames(start, end) {
    const params = new URLSearchParams();

    params.append('start', start);
    params.append('end', end);

    return api.get(`/Games?${params.toString()}`);
  },
};

export default GameApi;
