import routes from '../../constants/routes';
import statusCodes from '../../constants/statusCodes';

class ErrorHandler {
  constructor(navigate) {
    this.navigate = navigate;
  }

  handleError(error) {
    if (error.status === statusCodes.notFound) {
      this.navigate(routes.notFound);
    } else if (
      error.status === statusCodes.notAuthorized ||
      error.status === statusCodes.forbidden
    ) {
      this.navigate(routes.noAccess);
    } else {
      this.navigate(routes.error);
    }
  }
}

export default ErrorHandler;
