import endpointConstants from '../constants/endpointConstants';
import api from './Api';

const GameApi = {
  async getAnimalTypes() {
    return api.get(endpointConstants.animalTypes);
  },
};

export default GameApi;
