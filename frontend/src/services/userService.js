import axios from "axios";

class UserService {
  constructor(config) {
    this.user = JSON.parse(localStorage.getItem("user"));

    this.api = axios.create({
      baseURL: config.apiUrl,
      headers: {
        "Content-Type": "application/json",
      },
    });
  }

  /**
   * Get a user by their ID
   * @param {number} userId - The ID of the user to retrieve
   * @returns {Promise<Object>} The user data
   * @throws {Error} If the request fails
   */
  async getUser() {
    try {
      const response = await this.api.get(`/users/${this.user.userId}`);
      return response.data;
    } catch (error) {
      throw this.handleError(error, "Failed to fetch user");
    }
  }

  /**
   * Update an existing user
   * @param {number} userId - The ID of the user to update
   * @param {Object} updateUserDto - The user data to update
   * @returns {Promise<void>}
   * @throws {Error} If the request fails
   */
  async updateUser(updateUserDto) {
    try {
      return await this.api.put(`/users/${this.user.userId}`, updateUserDto);
    } catch (error) {
      throw this.handleError(error, "Failed to update user");
    }
  }

  /**
   * Handle API errors
   * @private
   * @param {Error} error - The error object from axios
   * @param {string} defaultMessage - Default error message
   * @returns {Error} Processed error object
   */
  handleError(error, defaultMessage) {
    if (axios.isAxiosError(error)) {
      const message = error.response?.data?.message || error.message;
      return new Error(`${defaultMessage}: ${message}`);
    }
    return error;
  }
}

export default UserService;
