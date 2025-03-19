import endpointConstants from '../constants/endpointConstants';
import api from './Api';

const AccountApi = {
  async login(loginData) {
    return api.post(endpointConstants.login, loginData);
  },

  async register(registerData) {
    return api.post(endpointConstants.register, registerData);
  },

  async accessToken() {
    return api.post(endpointConstants.accessToken);
  },
  async logout() {
    return api.post(endpointConstants.logout);
  },
};

export default AccountApi;
