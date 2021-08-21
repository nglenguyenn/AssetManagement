const Endpoints = {
    //account endpoints
    login: 'api/account/login',
    getRole: 'api/account/getaccountrole',
    authorize: 'api/authorize',
    changePassword: 'api/account/changepassword',
    me: 'api/account/getaccount',
    firstTimeChangePassword: 'api/account/changepasswordfirsttimelogin',

    //user endpoints
    createNewUser: '../api/user/create',
    getUsers: 'api/user/getlist',
    getUser: (id) => `/api/user/${id}`,
    editUser: '../api/User/edit',

    //asset endpoints
    createNewAsset: '../api/asset/create',
};

export default Endpoints;
