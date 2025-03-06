class TokenHandler {
  isTokenExpired(token) {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const currentTime = Math.floor(Date.now() / 1000);
      return payload.exp && payload.exp < currentTime;
    } catch (error) {
      console.log('Token validation failed', error.message);
      return true;
    }
  }

  tokenToExipre(token, bufferTimeInSeconds = 120) {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const currentTime = Date.now() / 1000;
      const expirationTime = payload.exp;
      return expirationTime - currentTime <= bufferTimeInSeconds;
    } catch (error) {
      console.log('Token validation failed', error.message);
      return true;
    }
  }
}

export default TokenHandler;
