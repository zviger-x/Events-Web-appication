import { useState } from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { UserDTO } from "../../Models/Users/UserDTO";
import APIConnector from "../../API/APIConnector";
import { Button, Form, Segment } from "semantic-ui-react";
import "./UserLRForm.css";
import TH from "../../API/TokenHandler";

export default function EventEditForm() {
    const navigate = useNavigate();
    const [user, setUser] = useState<UserDTO>({
        id: undefined,
        name: '',
        surname: '',
        birthDate: '',
        email: '',
        password: '',
        role: ''
    });
    const [errors, setErrors] = useState<{ [key: string]: string }>({});

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        if (user) {
            const { name, value } = e.target;
            setUser({ ...user, [name]: value });
            setErrors({ }); // Remove errors on change
        }
    };

    const validate = async (): Promise<boolean> => {
        const newErrors: { [key: string]: string } = {};
        if (!user.name) newErrors.name = "Name is required.";
        if (!user.surname) newErrors.surname = "Surname is required.";
        if (!user.birthDate) newErrors.birthDate = "Birth date is required.";
        if (!user.email) newErrors.email = "Email is required.";
        if (!user.password) newErrors.password = "Password is required.";

        // Check for duplicate event name
        if (user?.email) {
            const existingEvents = await APIConnector.GetUserByEmail(user.email);
            const isDuplicate = !!existingEvents;
            if (isDuplicate) {
                newErrors.email = "An user with this email already exists.";
                newErrors.emailDuplicate = "An user with this email already exists.";
            }
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (await validate()) {
            if (user) {
                await APIConnector.RegisterUser(user);
                await APIConnector.Login(user.email, user.password);
                let uid = TH.GetUserId(TH.ParseToken(TH.GetToken()!));
                navigate(`/account/${uid}`);
            }
        }
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
            <Segment className="form-container" style={{ display: 'inline-block', width: 'auto' }}>
                <div style={{ padding: '6px', textAlign: 'start' }}>
                    <h1>Register</h1>
                </div>
                <div className="event-details">
                    <Form onSubmit={handleSubmit}>
                        <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px' }}>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Name</label>
                                <Form.Input
                                    type="text"
                                    name="name"
                                    value={user.name}
                                    onChange={handleInputChange}
                                    error={!!errors.name}
                                    placeholder={errors.name || ''}
                                />
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Surname</label>
                                <Form.Input
                                    type="text"
                                    name="surname"
                                    value={user.surname}
                                    onChange={handleInputChange}
                                    error={!!errors.surname}
                                    placeholder={errors.surname || ''}
                                />
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Birth date</label>
                                <Form.Input
                                    type="datetime-local"
                                    name="birthDate"
                                    value={user.birthDate}
                                    onChange={handleInputChange}
                                    error={!!errors.birthDate}
                                    placeholder={errors.birthDate || ''}
                                />
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Email</label>
                                <Form.Input
                                    type="text"
                                    name="email"
                                    value={user.email}
                                    onChange={handleInputChange}
                                    error={!!errors.email}
                                    placeholder={errors.email || ''}
                                />
                                {errors.email && <div style={{ color: 'darkred' }}>{errors.email}</div>}
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Password</label>
                                <Form.Input
                                    type="text"
                                    name="password"
                                    value={user.password}
                                    onChange={handleInputChange}
                                    error={!!errors.password}
                                    placeholder={errors.password || ''}
                                />
                            </Form.Field>
                        </div>
                        <div style={{ paddingTop: '20px' }}>
                            <Button floated='right' type="submit" color="green">Create</Button>
                            <Button floated='right' as={NavLink} to="/events" color="grey">Cancel</Button>
                        </div>
                    </Form>
                </div>
            </Segment>
        </div>
    );
}