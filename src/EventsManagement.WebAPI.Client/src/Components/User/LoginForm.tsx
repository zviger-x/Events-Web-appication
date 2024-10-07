import { useState } from "react";
import { NavLink, useNavigate } from "react-router-dom";
import APIConnector from "../../API/APIConnector";
import { Button, Form, Segment } from "semantic-ui-react";
import "./UserLRForm.css";
import TH from "../../API/TokenHandler";

export default function LoginForm() {
    const navigate = useNavigate();
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [errors, setErrors] = useState<{ [key: string]: string }>({});

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        if (name === 'email') {
            setEmail(value);
        } else if (name === 'password') {
            setPassword(value);
        }
        setErrors({});
    };

    const validate = (): boolean => {
        const newErrors: { [key: string]: string } = {};
        if (!email) newErrors.email = "Email is required.";
        if (!password) newErrors.password = "Password is required.";
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (validate()) {
            try {
                await APIConnector.Login(email, password);
                let uid = TH.GetUserId(TH.ParseToken(TH.GetToken()!));
                navigate(`/account/${uid}`);
            } catch (error) {
                setErrors({ login: "Invalid email or password." });
            }
        }
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
            <Segment className="form-container" style={{ display: 'inline-block', width: 'auto' }}>
                <div style={{ padding: '6px', textAlign: 'start' }}>
                    <h1>Login</h1>
                </div>
                <div className="event-details">
                    <Form onSubmit={handleSubmit}>
                        <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px' }}>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Email</label>
                                <Form.Input
                                    type="text"
                                    name="email"
                                    value={email}
                                    onChange={handleInputChange}
                                    error={!!errors.email}
                                    placeholder={errors.email || ''}
                                />
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Password</label>
                                <Form.Input
                                    type="password"
                                    name="password"
                                    value={password}
                                    onChange={handleInputChange}
                                    error={!!errors.password}
                                    placeholder={errors.password || ''}
                                />
                            </Form.Field>
                        </div>
                        {errors.login && <div style={{ color: 'darkred', textAlign: 'center' }}>{errors.login}</div>}
                        <div style={{ paddingTop: '20px' }}>
                            <Button floated='right' type="submit" color="green">Login</Button>
                            <Button floated='right' as={NavLink} to="/account/register" color="grey">Register</Button>
                        </div>
                    </Form>
                </div>
            </Segment>
        </div>
    );
}
