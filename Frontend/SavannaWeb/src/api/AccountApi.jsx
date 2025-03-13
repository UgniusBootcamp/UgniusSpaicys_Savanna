import api from './Api';

const AccountApi = {
  async login(loginData) {
    return api.post('/Accounts/Login', loginData);
  },

  async register(registerData) {
    return api.post('/Accounts/Register', registerData);
  },

  async accessToken() {
    return api.post('/Accounts/AccessToken');
  },
  async logout() {
    return api.post('/Accounts/Logout');
  },
};

export default AccountApi;
