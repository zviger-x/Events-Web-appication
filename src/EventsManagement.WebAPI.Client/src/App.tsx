import { Link, Outlet, useLocation } from 'react-router-dom';
import './App.css';
import EventsTable from './Components/Events/EventsTable';
import { Container } from 'semantic-ui-react';
import { useEffect } from 'react';
import { SetupErrorHandlingInterceptor } from './Interceptors/AxiosInterceptor';
import UserLoginRegisterMinitab from './Components/User/UserLoginRegisterMinitab';

function App() {
    const location = useLocation();

    useEffect(() => {
        SetupErrorHandlingInterceptor();
    }, []);

    return (
        <>
            <div className="app-tab-container">
                <Link to="/">
                    <h1>Events management application</h1>
                </Link>
                <UserLoginRegisterMinitab />
            </div>
            {location.pathname === '/' ? <EventsTable /> : (
                <Container className="container-style">
                    <Outlet />
                </Container>
            )}
            <div style={{ margin: '150px', minHeight: '150px' }} />
        </>
    )
}

export default App;