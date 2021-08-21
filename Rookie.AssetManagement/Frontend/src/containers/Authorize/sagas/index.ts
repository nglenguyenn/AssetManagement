import { takeLatest, takeLeading } from 'redux-saga/effects';

import { changePassword, login, me, setRole, firstTimeChangePassword} from 'src/containers/Authorize/reducer';
import { handleLogin, handleGetMe, handleChangePassword, handleFirstTimeChangePassword } from 'src/containers/Authorize/sagas/handles';

export default function* authorizeSagas() {
    yield takeLatest(login.type, handleLogin),
    yield takeLatest(me.type, handleGetMe),
    yield takeLatest(changePassword.type, handleChangePassword)
    yield takeLatest(firstTimeChangePassword.type, handleFirstTimeChangePassword)
}
