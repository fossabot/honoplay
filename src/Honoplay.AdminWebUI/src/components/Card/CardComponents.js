import React from 'react';
import moment from 'moment';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Link } from "react-router-dom";
import { withStyles } from '@material-ui/core/styles';
import {
    Card,
    CardContent,
    Typography,
    Grid,
    IconButton,
    MuiThemeProvider,
    Button
} from '@material-ui/core';
import { Style, theme } from './Style';
import Modal from '../Modal/ModalComponent';
import DeleteOutlinedIcon from '@material-ui/icons/DeleteOutlined';

window.__MUI_USE_NEXT_TYPOGRAPHY_VARIANTS__ = true;

class CardComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            open: false,
        };
    }

    handleOpen = (id) => {
        this.setState({ open: true });
        this.props.id(id);
    };

    handleClose = () => {
        this.setState({ open: false });
    };

    render() {
        const { open } = this.state;
        const { classes, data, url, children, titleName } = this.props;
        return (

            <MuiThemeProvider theme={theme}>
                <Grid container spacing={24}>
                    <Grid item sm={12}>
                        <div className={classes.cardRoot}>
                            {data.map((data, i) => {
                                const dateToFormat = data.createdAt;
                                return (
                                    <Grid item xs={6} sm={3} key={data.id}>
                                        <Card className={classes.card} key={i}>
                                            <div className={classes.cardIcon}>
                                                <IconButton
                                                    key={data.id}>
                                                    <DeleteOutlinedIcon />
                                                </IconButton>
                                            </div>
                                            <CardContent>
                                                <Typography
                                                    variant="h6"
                                                    className={classes.cardLabel}>
                                                    {data.name}
                                                </Typography>
                                                <Typography
                                                    variant="h6"
                                                    className={classes.cardDate}>
                                                    {moment(dateToFormat).format("DD/MM/YYYY")}
                                                </Typography>
                                                <div className={classes.center}>
                                                    <Button
                                                        variant="contained"
                                                        component={url && Link}
                                                        to={url && `/honoplay/${url}/${data.id}`}
                                                        onClick={() => this.handleOpen(data.id)}
                                                    >
                                                        {translate('Edit')}
                                                    </Button>
                                                </div>
                                            </CardContent>
                                        </Card>
                                    </Grid>
                                );
                            })}
                        </div>
                    </Grid>
                </Grid>
                <Modal
                    titleName={titleName}
                    handleClose={this.handleClose}
                    open={open}
                >
                    {children}
                </Modal>
            </MuiThemeProvider>
        );
    }
}

export default withStyles(Style)(CardComponent);