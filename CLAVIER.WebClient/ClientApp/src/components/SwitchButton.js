import React, { Component } from 'react';
import App from '../App';

export class SwitchButton extends Component {
    
    requestRoleSwitch()
    {
        App.connection.invoke("RequestRoleSwitch").catch(err => console.error(err.toString()));
    }

    constructor(props) {
        super(props);
    }

    render() {

        if(this.props.switch)
        {
            return (
                <button className="switch-button active button" type="button" onClick={() => this.requestRoleSwitch()}>Switch Roles</button>
            );
        }
        else
        {
            return (
                <button className="switch-button inactive button" type="button" onClick={() => this.requestRoleSwitch()}>Switch Roles</button>
            );
        }
        
    }
}