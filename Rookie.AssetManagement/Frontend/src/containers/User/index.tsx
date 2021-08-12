import React, { lazy } from "react";
import { Route, Switch } from "react-router-dom";
import { CREATE_USER, USER } from "src/constants/pages";
import ListUser from "./List";

const CreateUser = lazy(() => import("../User/Create"));

const User = () => {
  return (
    <Switch>
      <Route exact path={USER}>
                <ListUser />
            </Route>
      <Route exact path={CREATE_USER}>
        <CreateUser />
      </Route>
    </Switch>
  );
};
export default User;