import React, { Component } from 'react';
import { NotesButton } from './NotesButton';
import { SwitchButton } from './SwitchButton';

export class Controls extends Component {

    render(){

        return (<div id="control-wraper">
            <SwitchButton switch={this.props.switch}/>
            {this.props.role === "driver" ?
                <NotesButton render={this.props.render}/>:
                <div></div>
            }
            <div className="user-role">Your Role: <span id="role">{this.props.role}</span></div>
            </div>);
    }
}