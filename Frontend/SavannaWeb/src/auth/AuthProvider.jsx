import PropTypes from 'prop-types';
import { useCallback, useEffect, useMemo, useState } from 'react';
import accountApi from '../api/AccountApi';
import TokenHandler from '../helpers/TokenHandler';
import { AuthContext } from './AuthContext';

export const AuthProvider = ({ children }) => {
  const [roles, setRoles] = useState([]);
  const [user, setUser] = useState(null);
  const [userName, setUserName] = useState(null);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [snackbarMessage, setSnackbarMessage] = useState('');

  const tokenHandler = new TokenHandler();

  const logOut = useCallback(async () => {
    if (isLoggedIn) {
      await accountApi.logout();
    }
    setIsLoggedIn(false);
    setRoles([]);
    setUser(null);
    setUserName(null);

    localStorage.removeItem('accessToken');
  }, [isLoggedIn]);

  const fetchAccessToken = useCallback(async () => {
    try {
      const response = await accountApi.accessToken();
      const token = response.data.accessToken;
      parseToken(token);
      localStorage.setItem('accessToken', token);
    } catch (error) {
      console.error('Failed to fetch access token');
      await logOut();
    }
  }, []);

  const parseToken = (token) => {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const roles = [
        payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
      ];
      const user = payload.sub;
      const userName =
        payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];

      setRoles(roles || []);
      setUser(user);
      setUserName(userName);
      setIsLoggedIn(true);
    } catch (error) {
      console.error('Failed to parse token');
    }
  };

  useEffect(() => {
    const initializeAuth = async () => {
      const storedToken = localStorage.getItem('accessToken');

      if (
        storedToken &&
        !tokenHandler.isTokenExpired(storedToken) &&
        tokenHandler.tokenToExipre(storedToken)
      ) {
        parseToken(storedToken);
      } else {
        try {
          await fetchAccessToken();
        } catch (error) {
          console.error('Failed to fetch access token');
          await logOut();
        }
      }
      setIsLoading(false);
    };

    initializeAuth();

    const interval = setInterval(() => {
      fetchAccessToken();
    }, 1000 * 60 * 5);

    return () => clearInterval(interval);
  }, [fetchAccessToken, logOut]);

  const login = useCallback(
    async (formData) => {
      try {
        const response = await accountApi.login(formData);
        const token = response.data.accessToken;
        parseToken(token);
        localStorage.setItem('accessToken', token);
      } catch (error) {
        await logOut();
        throw error;
      }
    },
    [logOut],
  );

  const contextValue = useMemo(
    () => ({
      roles,
      user,
      userName,
      isLoggedIn,
      isLoading,
      fetchAccessToken,
      logOut,
      login,
      snackbarMessage,
      setSnackbarMessage,
    }),
    [
      roles,
      user,
      userName,
      isLoggedIn,
      isLoading,
      fetchAccessToken,
      logOut,
      login,
      snackbarMessage,
      setSnackbarMessage,
    ],
  );

  return (
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
};

AuthProvider.PropTypes = {
  children: PropTypes.node.isRequired,
};
