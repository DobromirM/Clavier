import React, {Component} from 'react';

export class Error extends Component {
    render() {
        return (
            <div className="error">
                <div className="error-text">
                    <div className="error-message">Unable to connect to the group.</div>
                </div>
            </div>
        );
    }
}
