import api from './Api';

const GameApi = {
  async getAnimalTypes() {
    return api.get('/Games/AnimalTypes');
  },
};

export default GameApi;
