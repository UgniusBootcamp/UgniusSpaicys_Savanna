import routes from '../../constants/routes';

class ErrorHandler {
  constructor(navigate) {
    this.navigate = navigate;
  }

  handleError(error) {
    if (error.status === 404) {
      this.navigate(routes.notFound);
    } else if (error.status === 403 || error.status === 401) {
      this.navigate(routes.noAccess);
    } else {
      this.navigate(routes.error);
    }
  }
}

export default ErrorHandler;
