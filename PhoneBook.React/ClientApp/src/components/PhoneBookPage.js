import React, { Component } from 'react';
import './PhoneBookPage.css';

class PhoneBookPage extends Component {
    static displayName = PhoneBookPage.firstName + PhoneBookPage.lastName;

    constructor(props) {
        super(props);
        this.state = {
            phoneBookList: [],
            loading: true,
            editUser: false,
            showPhoneBookUserOverlay: false,
            showContactOverlay: false,
            style: { width: "0%" },
            phoneBookUserDetailFromParent: {
                id: 0,
                firstName: "",
                lastName: ""
            },
            contactDetailsFromParent: {
                id: 0,
                firstName: "",
                lastName: "",
                phoneNumber: "",
                phoneUserId: 0,
                phoneBookUser: {
                    id: 0,
                    firstName: "",
                    lastName: ""
                }
            }
        };
        //this.handleContactOverlay = this.handleContactOverlay.bind(this);
        this.handlePhoneBookUserOverlay = this.handlePhoneBookUserOverlay.bind(this);
    }
    handleContactOverlay(e, phoneBook, contact, shouldDelete) {
        if (shouldDelete) {
            this.deleteContact(contact);
        }
        else if (contact != null) {
            this.setState({
                phoneBookUserDetailFromParent: this.state.phoneBookUserDetailFromParent,
                showContactOverlay: true,
                style: { width: "100%" },
                showPhoneBookUserOverlay: this.state.showPhoneBookUserOverlay,
                phoneBookList: this.state.phoneBookList,
                loading: this.state.loading,
                editUser: this.state.editUser,
                contactDetailsFromParent: {
                    ...contact, phoneUserId: phoneBook.id
                }
            });
        } else {
            this.setState({
                phoneBookUserDetailFromParent: this.state.phoneBookUserDetailFromParent,
                showContactOverlay: true,
                style: { width: "100%" },
                showPhoneBookUserOverlay: this.state.showPhoneBookUserOverlay,
                phoneBookList: this.state.phoneBookList,
                loading: this.state.loading,
                editUser: this.state.editUser,
                contactDetailsFromParent: {
                    id: 0,
                    firstName: "",
                    lastName: "",
                    phoneNumber: "",
                    phoneUserId: phoneBook.id,
                    phoneBookUser: phoneBook
                }
            });
        }
    }
    handlePhoneBookUserOverlay(e, phoneBook, shouldDelete) {
        if (shouldDelete) {
            this.deletePhoneBookUser(phoneBook);
        }
        else if (phoneBook != null) {
            this.setState({
                phoneBookUserDetailFromParent: phoneBook,
                showPhoneBookUserOverlay: true,
                style: { width: "100%" },
                phoneBookList: this.state.phoneBookList,
                loading: this.state.loading,
                showContactOverlay: this.state.showContactOverlay,
                editUser: this.state.editUser,
                contactDetailsFromParent: this.state.contactDetailsFromParent
            });
        } else {
            this.setState({
                phoneBookUserDetailFromParent: {
                    firstName: "",
                    lastName: ""
                },
                showContactOverlay: this.state.showContactOverlay,
                style: { width: "100%" },
                showPhoneBookUserOverlay: true,
                phoneBookList: this.state.phoneBookList,
                loading: this.state.loading,
                editUser: this.state.editUser,
                contactDetailsFromParent: this.state.contactDetailsFromParent
            });
        }
    }
    componentDidMount() {
        this.populatePhoneBookData();
    }
    renderPhoneBookTable(phoneBookList, canEdit) {
        return (
            <div className="container">
                {
                    phoneBookList.map((phoneBook) => {
                        return (
                            <div className="card-group">
                                <div className="custom-row" key={phoneBook.id}>
                                    <div className="col"><h1>{phoneBook.firstName} {phoneBook.lastName}</h1></div>
                                    <div className="col"><p className="bi bi-edit-circle" data-placement="bottom" title="Edit phone book user." onClick={(e) => this.handlePhoneBookUserOverlay(e, phoneBook, false)}></p></div>
                                    <div className="bi-delete-icon" data-placement="bottom" title="Delete phone book user." onClick={(e) => this.handlePhoneBookUserOverlay(e, phoneBook, true)}></div>
                                </div>
                                {(phoneBook.contacts) && (phoneBook.contacts.length > 0)
                                    ? <>
                                        <div className="col-md-3" >
                                            <div className="card">
                                                <div className="card-body">
                                                    <h5 className="card-title">{phoneBook.firstName} {phoneBook.lastName}</h5>
                                                    <h6 className="card-subtitle mb-2 text-muted">Add New Contact</h6>
                                                    <div className="card-text">
                                                        <div className="bi-plus-circle" data-placement="bottom" title="Add new contact" onClick={(e) => this.handleContactOverlay(e, phoneBook, null, false)}></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        {
                                            phoneBook.contacts.map((contact) => {
                                                return (
                                                    <div className="col-md-3" >
                                                        <div className="card" key={contact.id}>
                                                            <div className="card-body">
                                                                <h5 className="card-title">{contact.firstName} {contact.lastName}</h5>
                                                                <h6 className="card-subtitle mb-2 text-muted">{contact.phoneNumber}</h6>
                                                                <div className="card-text">
                                                                    <div className="bi-edit-circle" data-placement="bottom" title="Edit contact." onClick={(e) => this.handleContactOverlay(e, phoneBook, contact, false)}></div>
                                                                    <div className="bi-delete-icon" data-placement="bottom" title="Add new contact" onClick={(e) => this.handleContactOverlay(e, phoneBook, contact, true)}></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                )
                                            })
                                        }
                                    </>
                                    : <div className="col-md-3" >
                                        <div className="card">
                                            <div className="card-body">
                                                <h5 className="card-title">{phoneBook.firstName} {phoneBook.lastName}</h5>
                                                <h6 className="card-subtitle mb-2 text-muted">Add New Contact</h6>
                                                <div className="card-text">
                                                    <div className="bi-plus-circle" onClick={(e) => this.handleContactOverlay(e, phoneBook, null, false)} data-placement="bottom" title="Add new contact"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>}
                            </div>

                        )
                    })
                }
            </div>
        );
    }

    phoneBookUserOverlayCallbackFunction = (style, showPhoneBookUserOverlay) => {
        this.setState({
            phoneBookUserDetailFromParent: this.state.phoneBookUserDetailFromParent,
            showPhoneBookUserOverlay: showPhoneBookUserOverlay,
            style: style,
            phoneBookList: this.state.phoneBookList,
            loading: this.state.loading,
            showContactOverlay: this.state.showContactOverlay,
            editUser: this.state.editUser,
            contactDetailsFromParent: this.state.contactDetailsFromParent
        });
        this.populatePhoneBookData();
    }

    contactOverlayCallbackFunction = (style, showContactOverlay) => {
        this.setState({
            phoneBookUserDetailFromParent: this.state.phoneBookUserDetailFromParent,
            showContactOverlay: showContactOverlay,
            style: style,
            phoneBookList: this.state.phoneBookList,
            loading: this.state.loading,
            showPhoneBookUserOverlay: this.state.showPhoneBookUserOverlay,
            editUser: this.state.editUser,
            contactDetailsFromParent: this.state.contactDetailsFromParent
        });
        this.populatePhoneBookData();
    }

    render() {
        let canEdit = this.state.editUser;
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderPhoneBookTable(this.state.phoneBookList, canEdit);

        return (

            <div>
                <div className="row">
                    <div className="col"><h1 id="tabelLabel">Phone Book</h1></div>
                    <div className="col" data-toggle="tooltip">
                        <p data-placement="bottom" title="Add new phone book user." className="bi bi-plus-circle" onClick={(e) => this.handlePhoneBookUserOverlay(e, null, false)}></p>
                    </div>
                </div>
                {contents}
                {!this.state.showPhoneBookUserOverlay ? "" : <PhoneBookUserOverlay
                    dataFromParent={this.state.style}
                    phoneBookUserDetailFromParent={this.state.phoneBookUserDetailFromParent}
                    phoneBookUserOverlayCallbackFunction={this.phoneBookUserOverlayCallbackFunction}
                />}
                {
                    !this.state.showContactOverlay ? "" : <ContactOverlay
                        dataFromParent={this.state.style}
                        contactDetailsFromParent={this.state.contactDetailsFromParent}
                        contactOverlayCallbackFunction={this.contactOverlayCallbackFunction} />
                }
            </div>
        );
    }

    async populatePhoneBookData() {
        const response = await fetch('phoneBook');
        const data = await response.json();
        this.setState({
            phoneBookList: data,
            loading: false,
            editUser: this.state.editUser,
            showPhoneBookUserOverlay: this.state.showPhoneBookUserOverlay,
            showContactOverlay: this.state.showContactOverlay,
            style: this.state.style,
            phoneBookUserDetailFromParent: this.state.phoneBookUserDetailFromParent,
            contactDetailsFromParent: this.state.contactDetailsFromParent
        });
    }

    async deletePhoneBookUser(phonebookUser) {
        const requestOptions = {
            method: 'delete',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(phonebookUser)
        };
        const response = await fetch(`phoneBook/${phonebookUser.id}`, requestOptions);
        const data = await response.json();
        this.populatePhoneBookData();
    }
    async deleteContact(contact) {
        const requestOptions = {
            method: 'delete',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(contact)
        };
        const response = await fetch(`contact/${contact.id}`, requestOptions);
        const data = await response.json();
        this.populatePhoneBookData();
    }
}


class PhoneBookUserOverlay extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            sucessMessage: "",
            isSuccessful: false,
            style: this.props.dataFromParent,
            newPhoneBookUserDetails: {
                id: this.props.phoneBookUserDetailFromParent.id,
                firstName: this.props.phoneBookUserDetailFromParent.firstName,
                lastName: this.props.phoneBookUserDetailFromParent.lastName,
            }
        };
        this.closeNav = this.closeNav.bind(this);
        this.handleFirstNameChange = this.handleFirstNameChange.bind(this);
        this.handleLastNameChange = this.handleLastNameChange.bind(this);
        this.handlePhoneBookUser = this.handlePhoneBookUser.bind(this);
    }
    handleFirstNameChange(e) {
        this.setState({
            newPhoneBookUserDetails: { ...this.state.newPhoneBookUserDetails, firstName: e.currentTarget.value }
        });
    }
    handleLastNameChange(e) {
        this.setState({
            newPhoneBookUserDetails: { ...this.state.newPhoneBookUserDetails, lastName: e.currentTarget.value }
        });
    }

    componentDidMount() {
        this.setState({ isSuccessful: this.state.isSuccessful, style: this.state.style, newPhoneBookUserDetails: this.state.newPhoneBookUserDetails });
    }

    closeNav(e) {
        e.preventDefault();
        const style = {
            width: "0%"
        };
        this.setState({
            sucessMessage: this.state.successMessage,
            isSuccessful: this.state.isSuccessful,
            style: style,
            newPhoneBookUserDetails: this.state.newPhoneBookUserDetails
        });
        this.props.phoneBookUserOverlayCallbackFunction(style, false);
    }

    handlePhoneBookUser(e) {
        e.preventDefault();
        if (this.state.newPhoneBookUserDetails.id > 0) {
            this.editPhoneBookUser(this.state.newPhoneBookUserDetails);
        } else {
            const details = {
                id: this.state.newPhoneBookUserDetails.id,
                firstName: this.state.newPhoneBookUserDetails.firstName,
                lastName: this.state.newPhoneBookUserDetails.lastName
            };
            this.addPhoneBookUser(details);
        }
    }
    render() {
        return (
            <div id="myNav" className="overlay" style={this.state.style}>
                {!this.state.isSuccessful ? "" : <div className="alert alert-success" role="alert">

                </div>}
                <p className="closebtn" onClick={this.closeNav}>&times;</p>
                <div className="overlay-content">
                    <div className="col-md-12 col-lg-12">
                        <h1>Phone Book User Page</h1>
                    </div>
                    <form className="col-lg-6 offset-lg-3">
                        <div className="form-group">
                            <label htmlFor="firstName">First Name</label>
                            <input
                                type="text"
                                className="form-control"
                                id="firstName"
                                placeholder="Enter first name"
                                value={this.state.newPhoneBookUserDetails.firstName}
                                onChange={this.handleFirstNameChange} />
                        </div>
                        <div className="form-group">
                            <label htmlFor="lastName">Last Name</label>
                            <input
                                type="text"
                                className="form-control"
                                id="lastName"
                                placeholder="Enter last name"
                                value={this.state.newPhoneBookUserDetails.lastName}
                                onChange={this.handleLastNameChange}
                            />
                        </div>
                        <button type="submit" className="btn btn-primary" onClick={this.handlePhoneBookUser}> Submit </button>
                    </form>
                </div>
            </div>
        );
    }
    async editPhoneBookUser(phonebookUser) {
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(phonebookUser)
        };
        const response = await fetch(`phoneBook/${phonebookUser.id}`, requestOptions);
        const data = await response.json();
        this.setState({
            successMessage: ` User: <b>${this.state.newPhoneBookUserDetails.firstName}  ${this.state.newPhoneBookUserDetails.lastName}</b> has been edited!`,
            isSuccessful: true,
            style: this.state.style,
            newPhoneBookUserDetails: this.state.newPhoneBookUserDetails
        });
        setTimeout(() => {
            const style = { width: "0%" };
            const showPhoneBookUserOverlay = false;
            this.setState({
                successMessage: "",
                isSuccessful: showPhoneBookUserOverlay,
                style: style,
                newPhoneBookUserDetails: this.state.newPhoneBookUserDetails
            });
            this.props.phoneBookUserOverlayCallbackFunction(style, showPhoneBookUserOverlay);
        }, 3000);
    }
    async addPhoneBookUser(phonebookUser) {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                "firstName": phonebookUser.firstName,
                "lastName": phonebookUser.lastName
            })
        };
        const response = await fetch('phoneBook', requestOptions);
        const data = await response.json();
        this.setState({
            successMessage: ` User: <b>${this.state.newPhoneBookUserDetails.firstName}  ${this.state.newPhoneBookUserDetails.lastName}</b> has been created!`,
            isSuccessful: true,
            style: this.state.style,
            newPhoneBookUserDetails: this.state.newPhoneBookUserDetails
        });
        setTimeout(() => {
            const style = { width: "0%" };
            const showPhoneBookUserOverlay = false;
            this.setState({
                successMessage: "",
                isSuccessful: showPhoneBookUserOverlay,
                style: style,
                newPhoneBookUserDetails: this.state.newPhoneBookUserDetails
            });
            this.props.phoneBookUserOverlayCallbackFunction(style, showPhoneBookUserOverlay);
        }, 3000);
    }
}

class ContactOverlay extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            sucessMessage: "",
            isSuccessful: false,
            style: this.props.dataFromParent,
            newContactDetails: {
                id: this.props.contactDetailsFromParent.id,
                firstName: this.props.contactDetailsFromParent.firstName,
                lastName: this.props.contactDetailsFromParent.lastName,
                phoneNumber: this.props.contactDetailsFromParent.phoneNumber,
                phoneUserId: this.props.contactDetailsFromParent.phoneUserId,
                phoneBookUser: this.props.contactDetailsFromParent.phoneBookUser,
            }
        };
        this.closeNav = this.closeNav.bind(this);
        this.handleContactFirstNameChange = this.handleContactFirstNameChange.bind(this);
        this.handleContactLastNameChange = this.handleContactLastNameChange.bind(this);
        this.handlePhoneNumberChange = this.handlePhoneNumberChange.bind(this);
        this.handleContact = this.handleContact.bind(this);
    }

    componentDidMount() {
        this.setState({ isSuccessful: this.state.isSuccessful, style: this.state.style, newContactDetails: this.state.newContactDetails });
    }
    closeNav(e) {
        e.preventDefault();
        const style = {
            width: "0%"
        };
        this.setState({
            sucessMessage: this.state.successMessage,
            isSuccessful: this.state.isSuccessful,
            style: style,
            newContactDetails: this.state.newContactDetails
        });
        this.props.contactOverlayCallbackFunction(style, false);
    }

    handleContact(e) {
        e.preventDefault();
        if (this.state.newContactDetails.id > 0) {
            this.editContact(this.state.newContactDetails);
        } else {
            const details = {
                id: this.state.newContactDetails.id,
                firstName: this.state.newContactDetails.firstName,
                lastName: this.state.newContactDetails.lastName,
                phoneNumber: this.state.newContactDetails.phoneNumber,
                phoneUserId: this.state.newContactDetails.phoneUserId,
                phoneBookUser: this.state.newContactDetails.phoneBookUser
            };
            this.addContact(details);
        }
    }
    handleContactFirstNameChange(e) {
        this.setState({
            newContactDetails: { ...this.state.newContactDetails, firstName: e.currentTarget.value }
        });
    }
    handleContactLastNameChange(e) {
        this.setState({
            newContactDetails: { ...this.state.newContactDetails, lastName: e.currentTarget.value }
        });
    }
    handlePhoneNumberChange(e) {
        this.setState({
            newContactDetails: { ...this.state.newContactDetails, phoneNumber: e.currentTarget.value }
        });
    }
    async editContact(contact) {
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                "id": contact.id,
                "firstName": contact.firstName,
                "lastName": contact.lastName,
                "phoneNumber": contact.phoneNumber,
                "phoneBookUserId": contact.phoneUserId
            })
        };
        const response = await fetch(`contact/${contact.id}`, requestOptions);
        const data = await response.json();
        this.setState({
            sucessMessage: `Contact: ${this.state.newContactDetails.firstName}  ${this.state.newContactDetails.lastName} has been Edited!`,
            isSuccessful: true,
            style: this.state.style,
            newContactDetails: data
        });
        setTimeout(() => {
            const style = { width: "0%" };
            const showContactOverlay = false;
            this.setState({
                sucessMessage: "",
                isSuccessful: showContactOverlay,
                style: style,
                newContactDetails: this.state.newContactDetails
            });
            this.props.contactOverlayCallbackFunction(style, showContactOverlay);
        }, 3000);
    }

    async addContact(contact) {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                "firstName": contact.firstName,
                "lastName": contact.lastName,
                "phoneNumber": contact.phoneNumber,
                "phoneBookUserId": contact.phoneBookUser.id
            })
        };
        const response = await fetch('contact', requestOptions);
        const data = await response.json();
        this.setState({
            sucessMessage: `Contact: ${this.state.newContactDetails.firstName}  ${this.state.newContactDetails.lastName} has been created!`,
            isSuccessful: true,
            style: this.state.style,
            newContactDetails: this.state.newContactDetails
        });
        setTimeout(() => {
            const style = { width: "0%" };
            const showContactOverlay = false;
            this.setState({
                sucessMessage: "",
                isSuccessful: showContactOverlay,
                style: style,
                newContactDetails: this.state.newContactDetails
            });
            this.props.contactOverlayCallbackFunction(style, showContactOverlay);
        }, 3000);
    }
    render() {
        return (
            <div id="myNav" className="overlay" style={this.state.style}>
                {!this.state.isSuccessful ? "" : <div className="alert alert-success" role="alert">
                    {this.state.sucessMessage}
                </div>}
                <p className="closebtn" onClick={this.closeNav}>&times;</p>
                <div className="overlay-content">
                    <div className="col-md-12 col-lg-12">
                        <h1>Contact Page</h1>
                    </div>
                    <form className="col-lg-6 offset-lg-3">
                        <div className="form-group">
                            <label htmlFor="phoneUserId">Phone User Id</label>
                            <input
                                type="number"
                                className="form-control"
                                id="phoneUserId"
                                placeholder="Enter Phone Number"
                                value={this.state.newContactDetails.phoneUserId}
                                readOnly />
                        </div>
                        <div className="form-group">
                            <label htmlFor="contactfirstName">First Name</label>
                            <input
                                type="text"
                                className="form-control"
                                id="contactfirstName"
                                placeholder="Enter first name"
                                value={this.state.newContactDetails.firstName}
                                onChange={this.handleContactFirstNameChange} />
                        </div>
                        <div className="form-group">
                            <label htmlFor="contactLastName">Last Name</label>
                            <input
                                type="text"
                                className="form-control"
                                id="contactLastName"
                                placeholder="Enter last name"
                                value={this.state.newContactDetails.lastName}
                                onChange={this.handleContactLastNameChange} />
                        </div>
                        <div className="form-group">
                            <label htmlFor="phoneNumber">Phone Number</label>
                            <input
                                type="text"
                                className="form-control"
                                id="phoneNumber"
                                aria-describedby="phoneNumberHelp"
                                placeholder="Enter Phone Number"
                                value={this.state.newContactDetails.phoneNumber}
                                onChange={this.handlePhoneNumberChange} />
                        </div>
                        <button type="submit" className="btn btn-primary" onClick={this.handleContact}>Submit</button>
                    </form>
                </div>
            </div>
        );
    }
}
export { PhoneBookPage, PhoneBookUserOverlay, ContactOverlay };