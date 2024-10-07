import { Button } from "semantic-ui-react";
import { NavLink } from "react-router-dom";
import TH from "../../API/TokenHandler";
import APIConnector from "../../API/APIConnector";

export default function EventsTableItem() {
    const parsedToken = TH.ParseToken(TH.GetToken()!);
    const userClaims = TH.GetAllClaims(parsedToken);

    return (
        <div>
            {userClaims ? (
                <>
                    <Button as={NavLink} to={`/account/${userClaims.id}`} size="tiny" type="button">{userClaims.name}</Button>
                    <Button size="tiny" onClick={() => { APIConnector.Logout(); location.reload(); }}>Logout</Button>
                </>
            ) : (
                <>
                    <Button as={NavLink} to="/account/login" size="tiny" type="button">Login</Button>
                    <Button as={NavLink} to="/account/register" size="tiny" type="button">Register</Button>
                </>
            )}
        </div>
    )
}