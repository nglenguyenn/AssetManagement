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
    getUsers: '../api/user/getlist',
    getUser: (id) => `/api/user/${id}`,
    editUser: '../api/User/edit',

    //asset endpoints
    createNewAsset: '../api/asset/create',
    editAsset: '../api/asset/edit',
    getAssets: '../api/asset/getlist',
    getAsset: (id) => `/api/asset/${id}`,
    getHistory: (id) => `api/asset/history?id=${id}`,

    //assignment endpoints
    getAssignments: '../api/assignment/getassignments',
    getAssignment: (id) => `/api/assignment/${id}`,
    createNewAssignment: '../api/assignment/create'
};

export default Endpoints;
